import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DietDTO } from './dietDTO';

@Injectable({
  providedIn: 'root',
})

export class DietsService {
  private commonHeaders = new HttpHeaders();
  private clientUrl: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.clientUrl = baseUrl + "getDiets";
    this.commonHeaders.set("Content-Type", "application/json");
  }

  public getDiets(): Promise<void | DietDTO[]> {
    return this.http.get<DietDTO[]>(this.clientUrl, { headers: this.commonHeaders, params: {} }).toPromise();
  }
}
