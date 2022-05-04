import { Routes } from "@angular/router";
import { AdminGuard } from "_guards/admin-guards";
import { AuthGuard } from "_guards/auth-guards";
import { HomeComponent } from "./home/home.component";
import { RegisterComponent } from "./register/register.component";
import { UserListComponent } from "./user-list/user-list.component";


export const appRoutes:Routes=[

    {path:"",component:HomeComponent},
    {path:"home",component:HomeComponent},
    {path:"users",component:UserListComponent,canActivate:[AdminGuard]},
     {path:"register",component:RegisterComponent,canActivate:[AdminGuard]},

    {path:"**",component:HomeComponent}


]
