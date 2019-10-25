import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {NgxChartsModule} from '@swimlane/ngx-charts';

import { AppRoutingModule } from './app-routing.module';
import { FinancialCoreModule } from './core/core.module';

import { AuthGuard } from './guards/auth.guard';
import { AlertService } from './core/local-services/alert.service';
import { SessionService } from './core/local-services/session.service';

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
import { DashboardComponent } from './dashboard/dashboard.component';


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
    UserCornerComponent,
    DashboardComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    CommonModule,
    FormsModule,
    BrowserAnimationsModule,
    NgbModule.forRoot(),
    FinancialCoreModule,
    NgxChartsModule
  ],
  providers: [
    AuthGuard,
    AlertService,
    SessionService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
