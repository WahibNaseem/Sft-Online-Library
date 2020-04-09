import { CheckoutPatron } from './checkoutPatron';

export interface CheckoutHistory {
    checkedOut: Date;
    checkedIn?: Date;
}