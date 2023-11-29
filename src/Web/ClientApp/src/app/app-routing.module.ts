import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { authGuard } from "./auth/auth.guard";
import { LoginComponent } from "./login/login.component";
import { RegistrationWizardComponent } from "./registration-wizard/registration-wizard.component";
import { NgModule } from "@angular/core";

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [authGuard],
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegistrationWizardComponent,
  },
]

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ApplicationRoutingModule { }
