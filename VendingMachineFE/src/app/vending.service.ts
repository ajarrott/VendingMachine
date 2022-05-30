import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { VendingMachine } from '../app/vending-models';
import { PurchaseModel, TransactionResponse, RefundResponse, RefundRequest } from './creditcard-model';
import { map, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VendingService {

  apiUrl = environment.vendingEnpoint;
  constructor(private http: HttpClient) { }

  getVendingMachine(){
    return this.http.get<VendingMachine>(this.apiUrl);
  }

  completePurchase(purchase: PurchaseModel) {
    return this.http.post<TransactionResponse>(this.apiUrl + '/purchase', purchase);
  }

  refundPurchase(refundId: number) {
    var refund: RefundRequest = {
      transactionId: refundId
    };

    console.log("refunding transaction" + refundId);

    return this.http.post<RefundResponse>(this.apiUrl + '/refund', refund);
      
  }
}
