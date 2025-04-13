import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from "../../entities/product";
import { ProductService } from "../../services/product.service";
import { UserService } from "../../services/user.service";
import { CartService } from "../../services/cart.service";

@Component({
  selector: 'app-collection',
  templateUrl: './collection.component.html',
  styleUrls: ['./collection.component.css']
})
export class CollectionComponent implements OnInit {
  products: Product[] = [];

  constructor(
    private productService: ProductService,
    public userService: UserService,
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getAllProducts();
  }

  onProductClick(productId: number) {
    console.log(productId);
  }

  onProductDelete(productId: number) {
    this.productService.deleteById(productId).subscribe(() => {
      this.getAllProducts();
    });
  }

  onProductAddById(productId: number) {
    this.productService.getById(productId).subscribe((product) => {
      this.cartService.setSelectedProductId(productId); // Set the selected product ID in the cart service
      this.router.navigate(['/cart']); // Navigate to the cart page
    });
  }

  getAllProducts() {
    this.productService.getAllProducts().subscribe((products) => {
      this.products = products;
      console.log("executed", this.products);
    });
  }

  addProduct(productId: number) {
    this.cartService.getById(productId).subscribe((product) => {
      this.router.navigate(['/cart'], { state: { product } });
    });
  }

  onProductClick2(productId: number): void {
    this.cartService.setSelectedProductId(productId);
  }
}
