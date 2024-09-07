import { Component, inject } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AuthService } from "../services/auth.service";
import { MatSnackBar } from "@angular/material/snack-bar";
import { AuthResponse } from "../models/user";
import { passwordMatchValidator } from "../../utils/password-match.validator";
import { Router } from "@angular/router";

@Component({
  selector: "app-login-register",
  templateUrl: "./login-register.component.html",
  styleUrls: ["./login-register.component.css"]
})
export class LoginRegisterComponent {
  form: FormGroup;
  isLoginMode = true;
  errorMessage: string = "";
  private _snackBar = inject(MatSnackBar);

  getValidators(formFieldName: string) {
    switch (formFieldName) {
      case "userName":
        return [Validators.required, Validators.minLength(2)];
      case "email":
        return [Validators.required, Validators.email];
      case "password":
        return [
          Validators.required,
          Validators.minLength(7),
          Validators.pattern(/[^\w\s]/),
          Validators.pattern(/\d/),
          Validators.pattern(/[A-Z]/)
        ];
      case "confirmPassword":
        return [Validators.required, passwordMatchValidator()];
      default:
        return null;
    }
  }

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.form = this.fb.group({
      userName: [""],
      email: [""],
      password: [""],
      confirmPassword: [""],
    });
  }
  foreachFormField = (func: ((formFieldName: string) => void)) => Object.keys(this.form.controls).forEach(func);
  resetAllErrors = () => Object.keys(this.form.controls).forEach(formFieldName => this.form.get(formFieldName)?.setErrors(null));
  clearAllValidators = () => this.foreachFormField(fieldName => this.form.get(fieldName)?.setValidators(null));
  setAllValidators = () => this.foreachFormField(fieldName => this.form.get(fieldName)?.setValidators(this.getValidators(fieldName)));

  afterAuthHandlers = {
    nextAuth: (value: AuthResponse): void => {
      if (value.token && value.email && value.userName) {
        localStorage.setItem("token", value.token);
        this.router.navigate([""]);
      }
    },
    errorAuth: (err: any) => {
      this._snackBar.open(err?.error?.message || err?.error || "Something Went Wrong!", "Close", {
        duration: 5000,
      });
      this.form.get("password")?.reset();
      this.form.get("confirmPassword")?.reset();
      this.form.enable();
    },
    completeAuth: () => this.form.enable()
  }


  toggleMode() {
    this.isLoginMode = !this.isLoginMode;
    if (this.isLoginMode) {
      this.clearAllValidators()
      this.form.get("userName")?.setValidators(Validators.required);
      this.form.get("password")?.setValidators(Validators.required);
    } else {
      this.setAllValidators();
    }
    this.form.setErrors(null);
    this.resetAllErrors();
    this.form.reset();
  }

  onSubmit() {
    if (this.form.invalid) return;
    this.form.disable();

    const { nextAuth, errorAuth, completeAuth } = this.afterAuthHandlers;

    if (this.isLoginMode) {
      const { userName, password } = this.form.value;
      this.authService.login({ userName, password }).subscribe(nextAuth, errorAuth, completeAuth);
    } else {
      if (this.form.value.password !== this.form.value.confirmPassword) {
        return;
      }
      const { userName, password, email } = this.form.value;
      this.authService.register({ userName, password, email }).subscribe(nextAuth, errorAuth, completeAuth);
    }
  }

  getPasswordErrorMessage(passwordFieldName: string) {
    const passwordControl = this.form.get(passwordFieldName);

    if (passwordControl?.hasError("required")) {
      return "Password is required";
    }

    if (passwordControl?.hasError("minlength")) {
      return "Password must be at least 7 characters long";
    }

    if (passwordControl?.hasError("pattern")) {
      if (!/[^\w\s]/.test(passwordControl?.value)) {
        return "Password must contain at least one non-alphanumeric character";
      }

      if (!/\d/.test(passwordControl?.value)) {
        return "Password must contain at least one digit";
      }

      if (!/[A-Z]/.test(passwordControl?.value)) {
        return "Password must contain at least one uppercase letter";
      }
    }

    return "";
  }
}
