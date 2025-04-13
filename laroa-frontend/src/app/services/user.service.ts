import { Injectable } from '@angular/core';
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { User } from "../entities/user";
import { LoginResponse } from "../entities/loginresponse";
import { RegisterResponse } from "../entities/register-response";
import {Order} from "../entities/order";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  userManagementBaseUrl = 'https://localhost:7066/api/users';
  deviceManagementBaseUrl = 'https://localhost:7066/api/users';

  constructor(private httpService: HttpClient, public router: Router) {}

  registerClient(client: { password: any; name: string; email: any }): Observable<RegisterResponse> {
    return this.httpService.post<RegisterResponse>(`${this.userManagementBaseUrl}/register-client`, client);
  }

  login(user: { password: any; email: any }) {
    return this.httpService.post<LoginResponse>(`${this.userManagementBaseUrl}/login`, user).subscribe((res: LoginResponse) => {
      localStorage.setItem('access_token', res.token);
      localStorage.setItem('email', res.email);
      localStorage.setItem('role', res.role);

      if (res.role === 'Admin') {
        this.router.navigate(['devices']).then(() => {
          window.location.reload();
        });
      } else if (res.role === 'Client') {
        this.router.navigate(['my-devices']).then(() => {
          window.location.reload();
        });
      }
    });
  }

  logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('email');
    localStorage.removeItem('role');
    this.router.navigate(['log-in']).then(() => {
      window.location.reload();
    });
  }

  getClientDevices(clientEmail: string): Observable<User> {
    return this.httpService.get<User>(`${this.deviceManagementBaseUrl}/${clientEmail}/user-devices`);
  }

  getClients(): Observable<User[]> {
    return this.httpService.get<User[]>(`${this.userManagementBaseUrl}/get-clients`);
  }

  updateClient(clientId: number, client: UserService): Observable<User> {
    return this.httpService.patch<User>(`${this.userManagementBaseUrl}/update-client/${clientId}`, client);
  }

  deleteClient(clientId: number): Observable<User> {
    return this.httpService.delete<User>(`${this.userManagementBaseUrl}/delete-client/${clientId}`);
  }

  addDeviceToClient(clientId: number, deviceId: number): Observable<User> {
    return this.httpService.post<User>(`${this.deviceManagementBaseUrl}/add-device-to-user/${clientId}/${deviceId}`, {});
  }

  removeDeviceFromClient(clientId: number, deviceId: number): Observable<User> {
    return this.httpService.delete<User>(`${this.deviceManagementBaseUrl}/remove-device-from-user/${clientId}/${deviceId}`);
  }

  getToken() {
    return localStorage.getItem('access_token');
  }

  getEmail() {
    return localStorage.getItem('email');
  }

  getRole() {
    return localStorage.getItem('role');
  }

  isLoggedIn() {
    return this.getToken() !== null;
  }

  isAdmin() {
    return this.getRole() === 'Admin';
  }

  isClient() {
    return this.getRole() === 'Client';
  }

  createOrder(): Observable<Order> {
    // Assuming you have an API endpoint for creating an order
    return this.httpService.post<Order>(`${this.userManagementBaseUrl}/create-order`, {});
  }

  addProductToOrder(orderId: number, productId: number): Observable<Order> {
    return this.httpService.post<Order>(`${this.userManagementBaseUrl}/add-product-to-order/${orderId}/${productId}`, {});
  }

  removeProductFromOrder(orderId: number, productId: number): Observable<Order> {
    return this.httpService.delete<Order>(`${this.userManagementBaseUrl}/remove-product-from-order/${orderId}/${productId}`);
  }
}
