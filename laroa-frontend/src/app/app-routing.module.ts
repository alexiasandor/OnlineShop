import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './features/products/products.component';
import {HomeComponent} from "./features/home/home.component";
import {ContactUsComponent} from "./features/contact-us/contact-us.component";
import {LoginComponent} from "./features/login/login.component";
import {CartComponent} from "./features/cart/cart.component";
import {AdminComponent} from "./features/admin/admin.component";
import {CollectionComponent} from "./features/collection/collection.component";
import {RegisterComponent} from "./features/register/register.component";
import {MessageComponent} from "./features/message/message.component";

const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: 'products',
    component: ProductsComponent
  },
  {
    path: 'collection',
    component: CollectionComponent
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'contact-us',
    component: ContactUsComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'cart',
    component: CartComponent
  },
  {
    path: 'admin',
    component: AdminComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'message',
    component: MessageComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
