import {ProductImage} from "./product-image";

export interface Product {
  id: number;
  name: string;
  price: number;
  priceR: number;
  description: string;
  stock: number;
  categoryId: number;
  productImage: ProductImage;
  // si alte proprietati
}
