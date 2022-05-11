import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DietDTO } from './dietDTO';
import { ProducerDietDTO } from './producerDietDTO';
import { EditDietDTO } from './editDietDTOs';

@Injectable({
  providedIn: 'root',
})

export class DietsService {
  private commonHeaders = new HttpHeaders();
  private dietUrl: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.dietUrl = baseUrl + "Diet/";
    this.commonHeaders.set("Content-Type", "application/json");
  }

  public getDiets(): Promise<void | DietDTO[]> {
    return this.http.get<DietDTO[]>(this.dietUrl + "getDiets", { headers: this.commonHeaders, params: {} }).toPromise();
  }

  public getProducerDiets(): Promise<void | ProducerDietDTO[]> {
    return this.http.get<ProducerDietDTO[]>(this.dietUrl + "getProducerDiets", { headers: this.commonHeaders, params: {} }).toPromise();
  }

  public getEditModelDiet(dietId: number): Promise<void | EditDietDTO> {
    return this.http.get<EditDietDTO>(this.dietUrl + "getEditDiets/" + dietId, { headers: this.commonHeaders, params: {} }).toPromise();
  }

  public deleteDiet(dietId: number): Promise<void> {
    return this.http.put<void>(this.dietUrl + "deleteDiet/" + dietId, { headers: this.commonHeaders, params: {} }).toPromise();
  }
}
