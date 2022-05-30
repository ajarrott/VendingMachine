import { Component, OnInit } from '@angular/core';
import { LedgerEntry } from '../ledger-models';
import { LedgerService } from '../ledger.service';

@Component({
  selector: 'app-ledger',
  templateUrl: './ledger.component.html',
  styleUrls: ['./ledger.component.css']
})
export class LedgerComponent implements OnInit {
  ledger: LedgerEntry[] | undefined;

  constructor(private ledgerService: LedgerService) { }

  ngOnInit(): void {
    this.getLedger()
  }

  getLedger() {
    this.ledgerService.getLedger()
      .subscribe((data: LedgerEntry[]) => this.ledger = data);
  }
}
