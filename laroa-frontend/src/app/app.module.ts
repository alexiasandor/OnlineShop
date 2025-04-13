import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
// @ts-ignore
import { ProductsComponent } from './features/products/products.component';
// @ts-ignore

import { HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./features/home/home.component";
import {ContactUsComponent} from "./features/contact-us/contact-us.component";
import {routes} from "./app.routes";
import {LoginComponent} from "./features/login/login.component";
import {CartComponent} from "./features/cart/cart.component";
import {AdminComponent} from "./features/admin/admin.component";
import {CollectionComponent} from "./features/collection/collection.component";
import {RegisterComponent} from "./features/register/register.component";
import {MessageComponent} from "./features/message/message.component";



@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    HomeComponent,
    ContactUsComponent,
    LoginComponent,
    CartComponent,
    AdminComponent,
    CollectionComponent,
    RegisterComponent,
    MessageComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes),
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
