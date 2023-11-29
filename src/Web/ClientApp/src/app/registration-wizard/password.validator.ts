import { AbstractControl, ValidatorFn } from '@angular/forms';

export function PasswordValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    if (!control.value) {
      return null;
    }

    const value: string = control.value;
    const digitRegex = /\d/;
    const letterRegex = /[a-zA-Z]/;

    const hasDigit = digitRegex.test(value);
    const hasLetter = letterRegex.test(value);

    if (!hasDigit || !hasLetter) {
      return { passwordInvalid: true };
    }

    return null;
  };
}
