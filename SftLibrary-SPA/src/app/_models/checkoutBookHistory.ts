import { Status } from './status';
import { CheckoutHistory } from './checkoutHistory';

export interface CheckoutBookHistory {
    id: number;
    title: string;
    author: string;
    year: number;
    status: Status;
    checkoutHistories?: CheckoutHistory[];
}