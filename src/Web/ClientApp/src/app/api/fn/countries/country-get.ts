/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { CountryModel } from '../../models/country-model';

export interface CountryGet$Params {
}

export function countryGet(http: HttpClient, rootUrl: string, params?: CountryGet$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<CountryModel>>> {
  const rb = new RequestBuilder(rootUrl, countryGet.PATH, 'get');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'text/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<Array<CountryModel>>;
    })
  );
}

countryGet.PATH = '/country';
