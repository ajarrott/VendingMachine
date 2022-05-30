import { ItemForSale } from "./vending-models"

export interface CreditCard {
    number: string,
    expirationMonth: number,
    expirationYear: number,
    expiration: Date
}

export interface PurchaseModel {
    card: CreditCard,
    itemsToPurchase: ItemForSale[],
    amountToCharge: number
}

export interface TransactionResponse {
    error: boolean,
    errorMessage: string,
    transactionId: number,
    creditCardApproved: boolean,
    transactionApproved: boolean,
    amountCharged: number
}

export interface RefundResponse {
    error: boolean,
    errorMessage: string,
    transactionId: number,
    amountRefunded: number,
    refundDate: Date
}

export interface RefundRequest{
    transactionId: number
}