import { Component, OnInit } from '@angular/core';
import { CartService } from '../cart.service';
import { RefundRequest, RefundResponse } from '../creditcard-model';
import { LedgerEntry } from '../ledger-models';
import { LedgerService } from '../ledger.service';
import { ItemForSale } from '../vending-models';
import { VendingService } from '../vending.service';
import { map, catchError } from 'rxjs';

@Component({
  selector: 'app-ledger',
  templateUrl: './ledger.component.html',
  styleUrls: ['./ledger.component.css']
})
export class LedgerComponent implements OnInit {
  ledger: LedgerEntry[] | undefined;
  refundResp: RefundResponse | undefined;

  constructor(private ledgerService: LedgerService, private vendingService: VendingService) { }

  ngOnInit(): void {
    this.getLedger()
  }

  getLedger() {
    this.ledgerService.getLedger()
      .subscribe((data: LedgerEntry[]) => this.ledger = data);
  }

  refund(id: number){
    this.vendingService.refundPurchase(id)
      .subscribe((data: RefundResponse) => this.refundResp = data);

    this.getLedger();
  }

  checkIfRefunded(date: Date): boolean {
    var isMinVal = this.checkIfMinVal(date);
    return isMinVal; // if its minimum value it has not been refunded, therefor should not be disabled
  }

  checkIfMinVal(date: Date): boolean {
    var dateVal = new Date(date);
    return dateVal.getDay() == 1 && dateVal.getFullYear() == 1 && dateVal.getMonth() == 0;
  }
}
