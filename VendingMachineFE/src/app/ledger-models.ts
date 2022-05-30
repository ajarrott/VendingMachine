import { ItemForSale } from "./vending-models";

export interface LedgerEntry {
    salePrice: number,
    saleDateTime: Date,
    refundDateTime: Date,
    lineItems: ItemForSale[],
    transactionId: number
}