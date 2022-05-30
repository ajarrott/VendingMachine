import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { LedgerEntry } from './ledger-models';

@Injectable({
  providedIn: 'root'
})
export class LedgerService {
  apiUrl = environment.ledgerEndpoint;
  constructor(private http: HttpClient) { }

  getLedger() {
    return this.http.get<LedgerEntry[]>(this.apiUrl);
  }
}
