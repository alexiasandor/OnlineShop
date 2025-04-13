// cart.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {Product} from "../entities/product";

@Injectable({
  providedIn: 'root',
})
export class CartService {
  productBaseUrl = "https://localhost:7066/api/products";
  private cartItemsSubject = new BehaviorSubject<string[]>([]);
  cartItems$ = this.cartItemsSubject.asObservable();
  private selectedProductIdSubject = new BehaviorSubject<number | null>(null);
  selectedProductId$ = this.selectedProductIdSubject.asObservable();

  constructor(private http: HttpClient) { }
  getById(productId:number): Observable<Product[]> {
    // const url  = `${this.productBaseUrl}/all`; daca e nevoie se mai adauga ceva la url
    return this.http.get<Product[]>(this.productBaseUrl+"/"+productId);
  }
  setSelectedProductId(productId: number): void {
    this.selectedProductIdSubject.next(productId);
  }
  getAllProducts(): Observable<Product[]> {
    // const url  = `${this.productBaseUrl}/all`; daca e nevoie se mai adauga ceva la url
    return this.http.get<Product[]>(this.productBaseUrl);
  }

}
