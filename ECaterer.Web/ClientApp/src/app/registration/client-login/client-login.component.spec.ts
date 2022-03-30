import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { GridModule } from '@progress/kendo-angular-grid';
import { ClientLogin } from './client-login.component';


describe('AppComponent', () => {
  let component: ClientLogin;
  let fixture: ComponentFixture<ClientLogin>;

  beforeAll(function () {
    jasmine.DEFAULT_TIMEOUT_INTERVAL = 999999;
  });

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ClientLogin],
      imports: [GridModule]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientLogin);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
