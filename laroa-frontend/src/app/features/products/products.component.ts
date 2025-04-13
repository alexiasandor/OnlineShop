import { Component} from '@angular/core';
import { Router, NavigationStart, NavigationEnd, Event as RouterEvent } from '@angular/router';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  productsArray: any[] = [
    { productName: 'Product 1', productPrice: 100, productImageUrl: 'path/to/product1-image.jpg' },
    // Add more product data here
  ];

  collections: any[] = [
    { name: 'Collection', imageUrl: 'assets/images/winter-collection.jpg' },

  ];

  showImage: boolean = true;

  constructor(private router: Router) { }

  ngOnInit() {
    this.router.events.subscribe((event: RouterEvent) => {
      if (event instanceof NavigationStart) {
        // Navigation is starting, hide the image
        this.showImage = false;
      } else if (event instanceof NavigationEnd) {
        // Navigation has ended, show the image
        this.showImage = true;
      }
    });
  }


}
