import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './guards/auth.guard';
import { PortalComponent } from './components/portal/portal.component';
import { LoginComponent } from './core/components/login/login.component';
import { AccountsListComponent } from './components/accounts-list/accounts-list.component';
import { EntriesListComponent } from './components/entries-list/entries-list.component';
import { EntriesTemplatesListComponent } from './components/entries-templates-list/entries-templates-list.component';
import { MemosMappingsListComponent } from './components/memos-mappings-list/memos-mappings-list.component';
import { ResultsCentersListComponent } from './components/results-centers-list/results-centers-list.component';
import { ImportingComponent } from './components/importing/importing.component';
import { AccountsTotalizationsReportComponent } from './components/accounts-totalizations-report/accounts-totalizations-report.component';

const appRoutes: Routes = [
  { path: '', component: PortalComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'portal', component: PortalComponent, canActivate: [AuthGuard] },
  { path: 'accounts-list', component: AccountsListComponent, canActivate: [AuthGuard] },
  { path: 'results-centers-list', component: ResultsCentersListComponent, canActivate: [AuthGuard] },
  { path: 'accounts-totalizations-report', component: AccountsTotalizationsReportComponent, canActivate: [AuthGuard] },
  { path: 'entries-templates-list', component: EntriesTemplatesListComponent, canActivate: [AuthGuard] },
  { path: 'entries-list', component: EntriesListComponent, canActivate: [AuthGuard] },
  { path: 'importing', component: ImportingComponent, canActivate: [AuthGuard] },
  { path: 'memos-mappings-list', component: MemosMappingsListComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
];
@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
