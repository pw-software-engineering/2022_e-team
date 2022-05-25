import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DietDTO } from '../../diets/api/dietDTO';
import { DelivererOrderDTO, OrderDTO, PreviewOrderDTO, ProducerOrderDTO } from './orderDTO';
import { CookieOptions, CookieService } from 'ngx-cookie';

@Injectable({
  providedIn: 'root',
})

export class OrderService {
  private commonHeaders = new HttpHeaders();
  private dietUrl: string;
  private orderUrl: string;

  private cartDietsCookie: string = "CART_DIETS";

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.dietUrl = baseUrl + "Diet/";
    this.orderUrl = baseUrl + "Order/";
    this.commonHeaders.set("Content-Type", "application/json");
    this.getOrCreateCart();
  }
  
  public getDietsInCart(): Promise<void | DietDTO[]> {
    var dietIds: string[] = this.cookieService.get(this.cartDietsCookie).split(";").filter(d => d.length > 0);
    return this.http.put<DietDTO[]>(this.dietUrl + "getDietsWithIds", dietIds, { headers: this.commonHeaders, params: {} }).toPromise();
  }

  public putDietInCart(dietId: number): void {
    var dietIds: string[] = this.getOrCreateCart();;
    dietIds.push(dietId.toString());
    this.cookieService.put(this.cartDietsCookie, dietIds.join(";"));
    this.refreshCartCount();
  }

  public removeDietInCart(dietId: number): void {
    var dietIds: string[] = this.getOrCreateCart();
    dietIds = dietIds.filter(d => d != dietId.toString());
    this.cookieService.put(this.cartDietsCookie, dietIds.join(";"));
    this.refreshCartCount();
  }

  public sendOrder(order: OrderDTO): Promise<void> {
    return this.http.post<void>(this.orderUrl + "sendOrder", order, { headers: this.commonHeaders, params: {} })
      .toPromise().then(data => {
        this.cookieService.remove(this.cartDietsCookie);
        this.refreshCartCount();
        return data;
      });
  }

  public refreshCartCount() {
    var dietIds: string[] = this.getOrCreateCart();;
    $("#cartCount").text(dietIds.length.toString());
  }

  public getOrCreateCart(): string[] {
    var cookie = this.cookieService.get(this.cartDietsCookie);
    if (!cookie) {
      this.cookieService.put(this.cartDietsCookie, "");
      return [];
    }
    return cookie.split(";").filter(d => d.length > 0);
  }

  public getDelivererOrders(): Promise<DelivererOrderDTO[]> {
    return this.http.get<DelivererOrderDTO[]>(this.orderUrl + "getDelivererOrders", { headers: this.commonHeaders, params: {} }).toPromise();
  }

  public deliverOrder(orderNumber: string): Promise<void> {
    return this.http.patch<void>(this.orderUrl + `deliverOrder/${orderNumber}`, {}, { headers: this.commonHeaders, params: {} }).toPromise();
  }

  public getProducerOrders(): Promise<ProducerOrderDTO[]> {
    return this.http.get<ProducerOrderDTO[]>(this.orderUrl + "getProducerOrders", { headers: this.commonHeaders, params: {} }).toPromise();
  }

  public previewOrder(orderNumber: string): Promise<PreviewOrderDTO> {
    return this.http.get<PreviewOrderDTO>(this.orderUrl + "previewOrder/" + orderNumber, { headers: this.commonHeaders, params: {} }).toPromise();
  }
}
