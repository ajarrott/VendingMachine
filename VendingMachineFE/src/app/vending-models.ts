export interface VendingMachine {
    inventory: ItemForSale[];
}

export interface ItemForSale {
    name: string;
    price: number;
    quantity: number;
}