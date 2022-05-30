import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { LedgerComponent } from './ledger/ledger.component';
import { VendingComponent } from './vending/vending.component';

@NgModule({
  declarations: [
    AppComponent,
    LedgerComponent,
    VendingComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
