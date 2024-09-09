using AngularApp1.Server.Dtos;
using AngularApp1.Server.Interfaces;
using AngularApp1.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace AngularApp1.Server.Controllers
{
    [Route("api/subnet")]
    [ApiController]
    [Authorize]
    public class SubnetController : ControllerBase
    {
        private readonly ISubnetRepository _subnetRepository;

        private Func<string?> GetCurrentUserName;
        public SubnetController(ISubnetRepository subnetRepository)
        {
            GetCurrentUserName = () => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            _subnetRepository = subnetRepository;
        }

        public static async Task<List<string>> GenerateIPsAsync(string subnet)
        {
            return await Task.Run(() =>
            {
                var parts = subnet.Split('/');
                if (parts.Length != 2)
                {
                    throw new FormatException("Wrong IP format");
                }
                var ip = IPAddress.Parse(parts[0]);
                int prefixLength = int.Parse(parts[1]);

                uint ipAddress = BitConverter.ToUInt32(ip.GetAddressBytes().Reverse().ToArray(), 0);
                uint mask = ~(uint.MaxValue >> prefixLength);
                uint startIP = ipAddress & mask;
                uint endIP = ipAddress | ~mask;

                var ips = new List<string>();
                for (uint i = startIP + 1; i < endIP; i++)
                {
                    byte[] bytes = BitConverter.GetBytes(i);
                    Array.Reverse(bytes); // Convert from little-endian to big-endian
                    ips.Add(new IPAddress(bytes).ToString());
                }

                return ips;
            });
        }


        [HttpGet]
        public async Task<IActionResult> GetSubnets(bool withOwners = false, bool withIps = false)
        {
            try
            {
                var subnets = await _subnetRepository.GetSubnets(withOwners, withIps);
                return Ok(subnets.Select(s => new SubnetDto(s)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ips")]
        public async Task<IActionResult> GetSubnetIps(string subnetString)
        {
            var ips = await _subnetRepository.GetSubnetIps(subnetString);
            if (ips == null)
            {
                return NotFound();
            }
            return File(Encoding.UTF8.GetBytes(string.Join("\n", ips)), "text/plain", $"{subnetString}.txt");
        }


        [HttpPost]
        public async Task<IActionResult> CreateSubnet(string subnet)
        {
            var ownerUserName = GetCurrentUserName();
            if (ownerUserName == null)
            {
                return Unauthorized();
            }
            try
            {
                var success = await _subnetRepository.CreateSubnet(ownerUserName, subnet);
                if (success)
                {
                    return Ok();
                }
                return BadRequest("User not found");
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
