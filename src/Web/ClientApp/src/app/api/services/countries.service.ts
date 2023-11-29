/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';

import { countryGet } from '../fn/countries/country-get';
import { CountryGet$Params } from '../fn/countries/country-get';
import { countryIdProvincesGet } from '../fn/countries/country-id-provinces-get';
import { CountryIdProvincesGet$Params } from '../fn/countries/country-id-provinces-get';
import { CountryModel } from '../models/country-model';
import { ProvinceModel } from '../models/province-model';

@Injectable({ providedIn: 'root' })
export class CountriesService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `countryGet()` */
  static readonly CountryGetPath = '/country';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `countryGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  countryGet$Response(params?: CountryGet$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<CountryModel>>> {
    return countryGet(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `countryGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  countryGet(params?: CountryGet$Params, context?: HttpContext): Observable<Array<CountryModel>> {
    return this.countryGet$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<CountryModel>>): Array<CountryModel> => r.body)
    );
  }

  /** Path part for operation `countryIdProvincesGet()` */
  static readonly CountryIdProvincesGetPath = '/country/{id}/provinces';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `countryIdProvincesGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  countryIdProvincesGet$Response(params: CountryIdProvincesGet$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<ProvinceModel>>> {
    return countryIdProvincesGet(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `countryIdProvincesGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  countryIdProvincesGet(params: CountryIdProvincesGet$Params, context?: HttpContext): Observable<Array<ProvinceModel>> {
    return this.countryIdProvincesGet$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<ProvinceModel>>): Array<ProvinceModel> => r.body)
    );
  }

}
