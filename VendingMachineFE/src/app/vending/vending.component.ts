import { Component, OnInit } from '@angular/core';
import { VendingMachine } from '../vending-models';
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

  constructor(private vendingService: VendingService) { }

  ngOnInit(): void {
    this.getItems();
  }

  getItems(){
    this.vendingService.getVendingMachine()
      .subscribe((data: VendingMachine) => this.vendingMachine = data)
  }


}
