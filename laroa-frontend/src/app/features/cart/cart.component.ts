import { Component, OnInit } from '@angular/core';
import { CartService } from "../../services/cart.service";
import { ProductService } from "../../services/product.service";
import { Product } from "../../entities/product";

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent implements OnInit {
  products: Product[] = [];

  constructor(
    private cartService: CartService,
    private productService: ProductService
  ) {}

  ngOnInit() {
    // Load products from storage on component initialization
    const storedProducts = JSON.parse(localStorage.getItem('cartProducts') || '[]');
    this.products = storedProducts;

    this.cartService.selectedProductId$.subscribe((productId) => {
      if (productId !== null) {
        this.loadSelectedProduct(productId);
      }
    });
  }

  loadSelectedProduct(productId: number): void {
    this.productService.getById(productId).subscribe(
      (product) => {
        console.log("API Response:", product);
        if (product && Array.isArray(product)) {
          // Assuming the API returns an array of products
          this.products = [...this.products, ...product];
        } else if (product && product.id && product.name && product.price && product.priceR && product.productImage) {
          // If the API mistakenly returns a single product, wrap it in an array
          this.products = [...this.products, product];
        } else {
          console.error("Invalid response from getById API:", product);
        }

        // Save updated products to storage
        localStorage.setItem('cartProducts', JSON.stringify(this.products));
      },
      (error) => {
        console.error("Error loading product:", error);
      }
    );
  }

  onProductDelete(productId: number): void {
    // Remove the product from the array
    this.products = this.products.filter((product) => product.id !== productId);

    // Update storage with the modified products array
    localStorage.setItem('cartProducts', JSON.stringify(this.products));
  }

  onRemoveAll(): void {
    // Clear the products array
    this.products = [];

    // Update storage with an empty array
    localStorage.setItem('cartProducts', JSON.stringify(this.products));
  }
}
