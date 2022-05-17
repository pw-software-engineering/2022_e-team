import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from "@angular/router";
import { DietsService } from './api/diets.service';
import { RegistrationService } from '../registration/api/registration.service';

@Component({
  selector: 'app-diets',
  templateUrl: './diets.component.html',
  styleUrls: ['./diets.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class Diets implements OnInit {
  constructor(private TitleService: Title, private router: Router, private dietsService: DietsService, private registrationService: RegistrationService) {
    this.TitleService.setTitle("Lista dostępnych diet");
  }

  public diets: any;

  ngOnInit(): void {
    this.diets = this.dietsService.getDiets();
  }

  public goToDiets() {
    window.location.reload();
  }

  public goToCart() {
  }

  public goToDiet() {
  }

  public logout() {
    this.registrationService.logout();
    this.router.navigate(['/client/login']);
  }
}
