import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { VendingMachine } from '../app/vending-models';

@Injectable({
  providedIn: 'root'
})
export class VendingService {

  apiUrl = environment.vendingEnpoint;
  constructor(private http: HttpClient) { }

  getVendingMachine() {
    return this.http.get<VendingMachine>(this.apiUrl);
  }
}
