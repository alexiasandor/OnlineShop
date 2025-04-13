import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import {Admin} from "../entities/admin";

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  adminBaseUrl = "https://localhost:7066/api/admin";

  constructor(private http: HttpClient) { }

  getAllAdmins(): Observable<Admin[]> {
    // const url  = `${this.productBaseUrl}/all`; daca e nevoie se mai adauga ceva la url
    return this.http.get<Admin[]>(this.adminBaseUrl);
  }


}
