import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TokenService } from '../services/token.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  public readonly loginFormGroup = new FormGroup({
    login: new FormControl("", [Validators.email, Validators.required]),
    password: new FormControl("", Validators.required)
  });

  constructor(private readonly tokenService: TokenService) { }

  async submit() {
    if (!this.loginFormGroup.valid) {
      return;
    }

    await this.tokenService.login(this.loginFormGroup.value);
  }
}
