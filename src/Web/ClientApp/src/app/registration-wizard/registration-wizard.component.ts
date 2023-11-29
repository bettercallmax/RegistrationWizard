import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { CountriesService, UsersService } from '../api/services';
import { Observable, of, switchMap } from 'rxjs';
import { CountryModel, ProvinceModel, RegisterModel } from '../api/models';
import { PasswordValidator } from './password.validator';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration-wizard',
  templateUrl: './registration-wizard.component.html',
  styleUrls: ['./registration-wizard.component.css']
})
export class RegistrationWizardComponent implements OnInit {
  public currentStepIndex = 0;

  public readonly loginForm = new FormGroup({
    login: new FormControl('', Validators.email),
    password: new FormControl('', [Validators.minLength(6), PasswordValidator()]),
    confirmPassword: new FormControl('', Validators.minLength(6)),
    agreement: new FormControl(false, Validators.required)
  });

  public readonly countryForm = new FormGroup({
    countryId: new FormControl<number>(null, Validators.required),
    provinceId: new FormControl<number>(null, Validators.required)
  });

  public countries$: Observable<CountryModel[]>;
  public provinces$: Observable<ProvinceModel[]>;

  constructor(
    private readonly countryService: CountriesService,
    private readonly userService: UsersService,
    private readonly router: Router) {
    this.loginForm.addValidators(this.createCompareValidator(this.loginForm.controls.password, this.loginForm.controls.confirmPassword));
  }

  ngOnInit(): void {
    this.provinces$ = (this.countryForm.controls.countryId.valueChanges).pipe(
      switchMap(countryId => countryId ? this.countryService.countryIdProvincesGet({ id: countryId }) : of([])));

    this.countries$ = this.countryService.countryGet();
  }

  public nextStep() {
    this.currentStepIndex += 1;
  }

  public submit() {
    const registerModel = this.getModelFromControls();
    if (!registerModel) {
      return;
    }

    this.userService.userRegisterPost({
      body: registerModel
    }).subscribe(_ => this.router.navigate(['/login']));
  }

  getModelFromControls(): RegisterModel {
    if (!this.loginForm.valid || !this.countryForm.valid) {
      return null;
    }

    return {
      login: this.loginForm.value.login,
      password: this.loginForm.value.password,
      provinceId: this.countryForm.value.provinceId
    } as RegisterModel;
  }

  createCompareValidator(first: AbstractControl, second: AbstractControl): ValidatorFn {
    return () => {
      if (first.value !== second.value) {
        return { match_error: 'Password value does not match' };
      }

      return null;
    };
  }
}
