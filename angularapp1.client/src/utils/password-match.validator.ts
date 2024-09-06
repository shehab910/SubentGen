import { AbstractControl, FormGroup, ValidatorFn } from '@angular/forms';

export function passwordMatchValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const formGroup = control.parent as FormGroup;
    if (!formGroup) return null;

    const password = formGroup.get('password');
    const confirmPassword = control;

    if (password && confirmPassword && password.value !== confirmPassword.value) {
      return { passwordMismatch: true };
    }
    return null;
  };
}
