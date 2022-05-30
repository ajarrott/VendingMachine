import { Injectable } from '@angular/core';
import { ItemForSale } from './vending-models';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  items: ItemForSale[] = [];

  constructor() { }

  addItem(item: ItemForSale): number {
    for(var i = 0; i < this.items.length; i++){
      if(this.items[i].name == item.name){
        this.items[i].quantity += item.quantity
        return this.items[i].quantity;
      }
    }

    // add item if not found to update above
    this.items.push(item);
    return item.quantity;
  }

  removeItem(itemIndex: number): void {
    this.items.splice(itemIndex, 1);
  }

  totalOfItems(): number {
    var total = 0;
    for(var i = 0; i < this.items.length; i++){
      total += this.items[i].price * this.items[i].quantity;
    }
    return total;
  }

  clearCart(): void {
    this.items = [];
  }
}
