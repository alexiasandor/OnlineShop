import {Product} from "./product";

export interface Order {
    id: number;
    orderDate: Date;
    price: number;
    products: Product[];
    userId: number;
}
