import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule, registerLocaleData } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import localePT from '@angular/common/locales/global/pt'; 
registerLocaleData(localePT, 'pt-BR'); 

import { AppRoutingModule } from './app-routing.module';
import { FinancialCoreModule } from './core/core.module';

import { AuthGuard } from './guards/auth.guard';
import { AlertService } from './core/local-services/alert.service';
import { SessionService } from './core/local-services/session.service';
import { MyHttpInterceptor } from './core/services/my-http-interceptor';

import { AppComponent } from './app.component';
import { PortalComponent } from './components/portal/portal.component';
import { UserCornerComponent } from './components/user-corner/user-corner.component';
import { AccountsListComponent } from './components/accounts-list/accounts-list.component';
import { ResultsCentersListComponent } from './components/results-centers-list/results-centers-list.component';
import { EntriesTemplatesListComponent } from './components/entries-templates-list/entries-templates-list.component';
import { MemosMappingsListComponent } from './components/memos-mappings-list/memos-mappings-list.component';
import { ImportingComponent } from './components/importing/importing.component';
import { EntriesListComponent } from './components/entries-list/entries-list.component';
import { AccountsTotalizationsReportComponent } from './components/accounts-totalizations-report/accounts-totalizations-report.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountsListComponent,
    ResultsCentersListComponent,
    EntriesTemplatesListComponent,
    MemosMappingsListComponent,
    ImportingComponent,
    EntriesListComponent,
    AccountsTotalizationsReportComponent,
    PortalComponent,
    UserCornerComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    CommonModule,
    FormsModule,
    HttpClientModule,
    NgbTypeaheadModule,
    FinancialCoreModule
  ],
  providers: [
    AuthGuard,
    AlertService,
    SessionService,
      { provide: LOCALE_ID, useValue: 'pt-BR' },
	    { provide: 'environmentProvider', useValue: environment },
	    { provide: HTTP_INTERCEPTORS, useClass: MyHttpInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
