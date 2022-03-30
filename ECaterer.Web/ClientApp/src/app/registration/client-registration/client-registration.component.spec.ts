import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { GridModule } from '@progress/kendo-angular-grid';

import { ClientRegistration } from './client-registration.component';

describe('AppComponent', () => {
  let component: ClientRegistration;
  let fixture: ComponentFixture<ClientRegistration>;

  beforeAll(function () {
    jasmine.DEFAULT_TIMEOUT_INTERVAL = 999999;
  });

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ClientRegistration],
      imports: [GridModule]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientRegistration);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
