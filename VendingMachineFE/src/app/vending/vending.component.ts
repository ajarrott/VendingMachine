import { Component, OnInit } from '@angular/core';
import { CartService } from '../cart.service';
import { ItemForSale, VendingMachine } from '../vending-models';
import { VendingService } from '../vending.service';

@Component({
  selector: 'app-vending',
  templateUrl: './vending.component.html',
  styleUrls: ['./vending.component.css']
})
export class VendingComponent implements OnInit {
  vendingMachine: VendingMachine = {
    inventory: [
      {name: "Soda", price: 1.00, quantity: 12 },
      {name: "Chips", price: 2.00, quantity: 12 },
      {name: "Test", price: 3.00, quantity: 12 },
    ]
  };

  constructor(private vendingService: VendingService, private cartService: CartService) { }

  ngOnInit(): void {
    this.getItems();
  }

  getItems(): void {
    this.vendingService.getVendingMachine()
      .subscribe((data: VendingMachine) => this.vendingMachine = data)
  }

  addItem(name: string, price:number, quantity: number): void{
    console.log("adding: " + name);
    var item: ItemForSale = { name: name, quantity: quantity, price: price };
    this.cartService.addItem(item);
  }
}
