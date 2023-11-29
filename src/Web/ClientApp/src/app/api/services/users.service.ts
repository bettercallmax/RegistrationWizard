/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';

import { userLoginPost } from '../fn/users/user-login-post';
import { UserLoginPost$Params } from '../fn/users/user-login-post';
import { UserModel } from '../models/user-model';
import { userRegisterPost } from '../fn/users/user-register-post';
import { UserRegisterPost$Params } from '../fn/users/user-register-post';

@Injectable({ providedIn: 'root' })
export class UsersService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `userLoginPost()` */
  static readonly UserLoginPostPath = '/user/login';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `userLoginPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  userLoginPost$Response(params?: UserLoginPost$Params, context?: HttpContext): Observable<StrictHttpResponse<UserModel>> {
    return userLoginPost(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `userLoginPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  userLoginPost(params?: UserLoginPost$Params, context?: HttpContext): Observable<UserModel> {
    return this.userLoginPost$Response(params, context).pipe(
      map((r: StrictHttpResponse<UserModel>): UserModel => r.body)
    );
  }

  /** Path part for operation `userRegisterPost()` */
  static readonly UserRegisterPostPath = '/user/register';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `userRegisterPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  userRegisterPost$Response(params?: UserRegisterPost$Params, context?: HttpContext): Observable<StrictHttpResponse<UserModel>> {
    return userRegisterPost(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `userRegisterPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  userRegisterPost(params?: UserRegisterPost$Params, context?: HttpContext): Observable<UserModel> {
    return this.userRegisterPost$Response(params, context).pipe(
      map((r: StrictHttpResponse<UserModel>): UserModel => r.body)
    );
  }

}
