import { Component, Input, OnInit } from '@angular/core';
import { CartService } from '../cart.service';
import { ItemForSale } from '../vending-models';
import { CreditCard, PurchaseModel, TransactionResponse } from '../creditcard-model';
import { VendingService } from '../vending.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  showCartDetails: boolean = false;
  cc: CreditCard = {} as CreditCard;
  transactionResp: TransactionResponse | undefined;
  validData: boolean = false;

  constructor(private cartService: CartService, private vendingService: VendingService) { }

  ngOnInit(): void {
  }  

  toggleCartItems(): void {
    this.showCartDetails = !this.showCartDetails;
  }

  cartItems(): ItemForSale[] {
    return this.cartService.items;
  }

  totalOfItems(): number {
    return this.cartService.totalOfItems();
  }

  removeItem(index: number): void {
    return this.cartService.removeItem(index);
  }

  updateCCDate() {
    var newDate = new Date(this.cc.expirationYear, this.cc.expirationMonth);
    console.log(newDate);
    if(this.isValidDate(newDate)){
      this.cc.expiration = newDate;
    }

    this.validateData();
  }

  isValidDate(date: any): boolean{
    return date instanceof Date;
  }

  validateData(){
    var temp = (this.cc.number != null && this.cc.number.length > 0) && this.cc.expiration != null;
    console.log(temp)
    this.validData = temp; 
  }

  completePurchase() {
    var purchase: PurchaseModel = {
      card: this.cc,
      itemsToPurchase: this.cartService.items,
      amountToCharge: this.cartService.totalOfItems()
    };

    this.vendingService.completePurchase(purchase)
      .subscribe(
        {
          next: (data: TransactionResponse) => this.transactionResp = data,
          error: (error: TransactionResponse) => {
            console.log(error)
          }
        });
  }
}
