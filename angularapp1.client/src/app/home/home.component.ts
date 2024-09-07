import { Component } from '@angular/core';
import { SubnetService } from '../services/subnet.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  displayedColumns: string[] = ['subnet', 'link'];
  dataSource: any = [];

  constructor(private subnetService: SubnetService) {
    this.subnetService.getSubnets().subscribe(res => {
      this.subnetService.getSubnets().subscribe(res => {
        this.dataSource = res.map(subnet => {
          const cidr = `${subnet.firstIpAddress}/${subnet.subnetCIDR}`;
          return { subnet: cidr, cidrValue: cidr }; // Store the CIDR for button click reference
        });
      });
    });
  }

  clickHandler(cidr: string) {
    console.warn("Downloading for CIDR:", cidr);
    this.subnetService.getIps(cidr).subscribe(res => {
      const url = window.URL.createObjectURL(res);
      const a = document.createElement('a');
      a.href = url;
      a.download = `${cidr}-ips.txt`;
      document.body.appendChild(a);
      a.click();
      window.URL.revokeObjectURL(url);
    });
  }
}
