import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ProdutoCreateComponent } from './manutencao/produto-create/produto-create.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ProdutoEditComponent } from './manutencao/produto-edit/produto-edit.component';
import { LoginComponent } from './account/login.component';
import { LayoutComponent } from './account/layout.component';

const routes: Routes = [
  {
    path:"",
    component:HomeComponent
  },
  {
    path:"account/login",
    component: LoginComponent
  },
  {
    path:"produtos/create",
    component: ProdutoCreateComponent
  },
  {
    path:"produtos/edit/:id",
    component: ProdutoEditComponent
  }
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    LayoutComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    MatSnackBarModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
