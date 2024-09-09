import { Component } from '@angular/core';
import { saveAs } from "file-saver";
import { SubnetService } from '../services/subnet.service';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  displayedColumns: string[] = ['subnet', 'link'];
  dataSource: any = null;

  constructor(private subnetService: SubnetService, private snackBar: MatSnackBar) {
    this.subnetService.getSubnets().subscribe(res => {
      this.subnetService.getSubnets().subscribe(res => {
        this.dataSource = res.map(subnet => {
          const cidr = `${subnet.firstIpAddress}/${subnet.subnetCIDR}`;
          return { subnet: cidr, cidrValue: cidr }; // Store the CIDR for button click reference
        });
      });
    });
  }

  downloadButtonHandler(cidr: string) {
    this.snackBar.open(`Downloading IPs for ${cidr}`, 'Close', { duration: 2000 });
    this.subnetService.getIps(cidr).subscribe(res => {
      saveAs(res, `${cidr}-ips.txt`);
    });
  }
}
