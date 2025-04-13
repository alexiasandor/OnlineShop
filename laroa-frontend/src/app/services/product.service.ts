import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import {Product} from "../entities/product";

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  productBaseUrl = "https://localhost:7066/api/products";

  constructor(private http: HttpClient) { }

  getAllProducts(): Observable<Product[]> {
    // const url  = `${this.productBaseUrl}/all`; daca e nevoie se mai adauga ceva la url
    return this.http.get<Product[]>(this.productBaseUrl);
  }

  deleteById(productId:number): Observable<Product[]> {
    // const url  = `${this.productBaseUrl}/all`; daca e nevoie se mai adauga ceva la url
    return this.http.delete<Product[]>(this.productBaseUrl+"/"+productId);
  }

  getById(productId: number): Observable<Product> {
    // const url  = `${this.productBaseUrl}/all`; daca e nevoie se mai adauga ceva la url
    return this.http.get<Product>(this.productBaseUrl+"/"+productId);
  }

}
