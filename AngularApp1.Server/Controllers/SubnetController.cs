using AngularApp1.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace AngularApp1.Server.Controllers
{
    [Route("api/subnet")]
    [ApiController]
    public class SubnetController : ControllerBase
    {
        private readonly ISubnetRepository _subnetRepository;
        public SubnetController(ISubnetRepository subnetRepository)
        {
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
        public async Task<IActionResult> getIps(string subnet)
        {
            try
            {
                var ips = await GenerateIPsAsync(subnet);
                return Ok(ips);
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


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSubnet(string subnet)
        {
            var ownerUserName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
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
