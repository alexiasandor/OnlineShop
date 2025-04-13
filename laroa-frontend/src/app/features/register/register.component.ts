// Import necessary modules
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { UserService } from "../../services/user.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  hide: boolean = true;

  // Inject necessary services (Router, FormBuilder, UserService)
  constructor(
      private router: Router,
      private formBuilder: FormBuilder,
      private userService: UserService,
  ) {}

  // Initialize the form group in the ngOnInit lifecycle hook
  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(50),
        ],
      ],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(5)]],
    });
  }

  // Method to handle user registration
  onRegister() {
    // Check if the form is valid
    console.log(this.registerForm.valid);

    //if (this.registerForm.valid) {
      // Create an object with the user data from the form
      const userToRegister = {
        name: this.registerForm.value.name,
        email: this.registerForm.value.email,
        password: this.registerForm.value.password,
      };

      // Call the registerClient method from the UserService
      this.userService.registerClient(userToRegister).subscribe(
          (response) => {
            // Handle the response from the server
            if (response.success) {
              // Registration was successful
              console.log(`User registered with ID ${response.userId}`);
              // Optionally, navigate to the login page after successful registration
              this.navigateToLogIn();
            } else {
              // Registration failed
              console.error(`Registration failed: ${response.message}`);
            }
          },
          (error) => {
            // Handle HTTP request error
            console.error('Registration request failed:', error);
          }
      );
    //}
  }

  // Method to toggle password visibility
  toggleHide() {
    this.hide = !this.hide;
  }

  // Method to navigate to the login page
  navigateToLogIn() {
    this.router.navigate(['/log-in']);
  }
}
