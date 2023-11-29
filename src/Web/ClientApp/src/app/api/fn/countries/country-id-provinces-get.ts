/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { ProvinceModel } from '../../models/province-model';

export interface CountryIdProvincesGet$Params {
  id: number;
}

export function countryIdProvincesGet(http: HttpClient, rootUrl: string, params: CountryIdProvincesGet$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<ProvinceModel>>> {
  const rb = new RequestBuilder(rootUrl, countryIdProvincesGet.PATH, 'get');
  if (params) {
    rb.path('id', params.id, {});
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'text/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<Array<ProvinceModel>>;
    })
  );
}

countryIdProvincesGet.PATH = '/country/{id}/provinces';
