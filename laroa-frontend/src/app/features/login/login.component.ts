import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { UserService } from "../../services/user.service";
import { User } from "../../entities/user";
import {MessageService} from "../../services/message.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  logInForm!: FormGroup;

  hide: boolean = true;
  isLoggedIn: boolean = false;

  constructor(
      private router: Router,
      private formBuilder: FormBuilder,
      private userService: UserService,
      private messageService: MessageService
  ) {
  }

  ngOnInit(): void {
    this.logInForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });

    this.isLoggedIn = this.userService.isLoggedIn();
  }

  login() {
    const logInUser: { password: any; email: any } = {
      email: this.logInForm.value.email,
      password: this.logInForm.value.password,
    };

    this.userService.login(logInUser);


  }

  onLogOut() {
    this.userService.logout();
  }

  toggleHide() {
    this.hide = !this.hide;
  }

  onRegister() {
    this.router.navigate(['/register']);
  }

  showPopupMessage(message: string) {
    this.messageService.showMessage(message);
  }

}
