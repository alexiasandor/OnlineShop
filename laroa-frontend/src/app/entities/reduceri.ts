import {Product} from "./product";

export interface Reduceri {
    id: number;
    procent: number;
    perioada: Date;
    tip: string;
    products: Product[];

}
