import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ObservableInput } from 'rxjs';


type SubnetResponse = {
  subnetCIDR: string;
  firstIpAddress: string;
  ipAddresses: string[] | null;
  owners: any[];
};


@Injectable({
  providedIn: 'root'
})
export class SubnetService {

  constructor(private http: HttpClient) { }

  createSubnet(cidr: string) {
    const params: HttpParams = new HttpParams().set('subnet', cidr);
    return this.http.post("/api/subnet", {}, { responseType: 'text', params });
  }

  getIps(cidr: string) {
    const params: HttpParams = new HttpParams().set('subnetString', cidr);
    return this.http.get("/api/subnet/ips", { responseType: "blob", params });
  }

  getSubnets(withOwners = false, withIps = false): Observable<SubnetResponse[]> {
    const params: HttpParams = new HttpParams();
    params.set("withOwners", withOwners);
    params.set("withIps", withIps);
    return this.http.get<SubnetResponse[]>("/api/subnet", { params })
  }
}
