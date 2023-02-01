import{P as m,o as p,j as d,a as t,_ as C,Q as T,r as a,c as L,w as n,y as b,R as U,S as B,l as S,p as M,b as P,d as r}from"./index.a74f1f35.js";var h=m({name:"Lock"});const Q={xmlns:"http://www.w3.org/2000/svg",viewBox:"0 0 1024 1024"},A=t("path",{fill:"currentColor",d:"M224 448a32 32 0 0 0-32 32v384a32 32 0 0 0 32 32h576a32 32 0 0 0 32-32V480a32 32 0 0 0-32-32H224zm0-64h576a96 96 0 0 1 96 96v384a96 96 0 0 1-96 96H224a96 96 0 0 1-96-96V480a96 96 0 0 1 96-96z"},null,-1),H=t("path",{fill:"currentColor",d:"M512 544a32 32 0 0 1 32 32v192a32 32 0 1 1-64 0V576a32 32 0 0 1 32-32zm192-160v-64a192 192 0 1 0-384 0v64h384zM512 64a256 256 0 0 1 256 256v128H256V320A256 256 0 0 1 512 64z"},null,-1);function I(o,e,l,u,s,i){return p(),d("svg",Q,[A,H])}h.render=I;h.__file="packages/components/Lock.vue";const O=h;var f=m({name:"SuccessFilled"});const q={xmlns:"http://www.w3.org/2000/svg",viewBox:"0 0 1024 1024"},K=t("path",{fill:"currentColor",d:"M512 64a448 448 0 1 1 0 896 448 448 0 0 1 0-896zm-55.808 536.384-99.52-99.584a38.4 38.4 0 1 0-54.336 54.336l126.72 126.72a38.272 38.272 0 0 0 54.336 0l262.4-262.464a38.4 38.4 0 1 0-54.272-54.336L456.192 600.384z"},null,-1);function N(o,e,l,u,s,i){return p(),d("svg",q,[K])}f.render=N;f.__file="packages/components/SuccessFilled.vue";const R=f;var g=m({name:"User"});const j={xmlns:"http://www.w3.org/2000/svg",viewBox:"0 0 1024 1024"},D=t("path",{fill:"currentColor",d:"M512 512a192 192 0 1 0 0-384 192 192 0 0 0 0 384zm0 64a256 256 0 1 1 0-512 256 256 0 0 1 0 512zm320 320v-96a96 96 0 0 0-96-96H288a96 96 0 0 0-96 96v96a32 32 0 1 1-64 0v-96a160 160 0 0 1 160-160h448a160 160 0 0 1 160 160v96a32 32 0 1 1-64 0z"},null,-1);function E(o,e,l,u,s,i){return p(),d("svg",j,[D])}g.render=E;g.__file="packages/components/User.vue";const Z=g;var w=m({name:"View"});const G={xmlns:"http://www.w3.org/2000/svg",viewBox:"0 0 1024 1024"},J=t("path",{fill:"currentColor",d:"M512 160c320 0 512 352 512 352S832 864 512 864 0 512 0 512s192-352 512-352zm0 64c-225.28 0-384.128 208.064-436.8 288 52.608 79.872 211.456 288 436.8 288 225.28 0 384.128-208.064 436.8-288-52.608-79.872-211.456-288-436.8-288zm0 64a224 224 0 1 1 0 448 224 224 0 0 1 0-448zm0 64a160.192 160.192 0 0 0-160 160c0 88.192 71.744 160 160 160s160-71.808 160-160-71.744-160-160-160z"},null,-1);function W(o,e,l,u,s,i){return p(),d("svg",G,[J])}w.render=W;w.__file="packages/components/View.vue";const X=w;const Y={name:"Login",components:{View:X,SuccessFilled:R,User:Z,Lock:O},data(){return{loginForm:{username:"Admin",password:"111111"},loginRules:{username:[{required:!0,trigger:"blur"}],password:[{required:!0,trigger:"blur"}]},passwordType:"password",capsTooltip:!1,loading:!1,showDialog:!1,redirect:void 0,otherQuery:{}}},watch:{$route:{handler:function(o){const e=o.query;e&&(this.redirect=e.redirect,this.otherQuery=this.getOtherQuery(e))},immediate:!0}},mounted(){this.loginForm.username===""?this.$refs.username.focus():this.loginForm.password===""&&this.$refs.password.focus()},destroyed(){},methods:{...T("security",["login"]),checkCapslock(o){const{key:e}=o;this.capsTooltip=e&&e.length===1&&e>="A"&&e<="Z"},showPwd(){this.passwordType==="password"?this.passwordType="":this.passwordType="password",this.$nextTick(()=>{this.$refs.password.focus()})},async handleLogin(){this.$refs.loginForm.validate(o=>{if(o)this.loading=!0,this.login(this.loginForm).then(()=>{this.loading=!1,this.$router.push("/")}).catch(()=>{this.loading=!1});else return console.log("error submit!!"),!1})},getOtherQuery(o){return Object.keys(o).reduce((e,l)=>(l!=="redirect"&&(e[l]=o[l]),e),{})}}},x=o=>(M("data-v-f2040ee2"),o=o(),P(),o),ee={class:"login-container"},oe=x(()=>r("div",{class:"title-container"},[r("h3",{class:"title"},"Login Form")],-1)),se=x(()=>r("div",{style:{position:"relative"}},[r("div",{class:"tips"},[r("span",null,"Username : admin"),r("span",null,"Password : any")]),r("div",{class:"tips"},[r("span",{style:{"margin-right":"18px"}},"Username : editor"),r("span",null,"Password : any")])],-1));function te(o,e,l,u,s,i){const $=a("User"),_=a("el-icon"),v=a("el-input"),y=a("el-form-item"),k=a("Lock"),V=a("el-tooltip"),F=a("el-button"),z=a("el-form");return p(),L("div",ee,[t(z,{ref:"loginForm",model:s.loginForm,rules:s.loginRules,class:"login-form",autocomplete:"on","label-position":"left"},{default:n(()=>[oe,t(y,{prop:"username"},{default:n(()=>[t(v,{ref:"username",modelValue:s.loginForm.username,"onUpdate:modelValue":e[0]||(e[0]=c=>s.loginForm.username=c),placeholder:"Username",name:"username",type:"text",tabindex:"1",autocomplete:"on"},{prepend:n(()=>[t(_,null,{default:n(()=>[t($)]),_:1})]),_:1},8,["modelValue"])]),_:1}),t(V,{modelValue:s.capsTooltip,"onUpdate:modelValue":e[3]||(e[3]=c=>s.capsTooltip=c),content:"Caps lock is On",placement:"right",manual:""},{default:n(()=>[t(y,{prop:"password"},{default:n(()=>[(p(),d(v,{key:s.passwordType,ref:"password",modelValue:s.loginForm.password,"onUpdate:modelValue":e[1]||(e[1]=c=>s.loginForm.password=c),type:s.passwordType,placeholder:"Password",name:"password",tabindex:"2",autocomplete:"on",onKeyup:[i.checkCapslock,U(i.handleLogin,["enter"])],onBlur:e[2]||(e[2]=c=>s.capsTooltip=!1)},{prepend:n(()=>[t(_,null,{default:n(()=>[t(k)]),_:1})]),append:n(()=>[t(_,null,{default:n(()=>[(p(),d(b(s.passwordType==="password"?"View":"SuccessFilled")))]),_:1})]),_:1},8,["modelValue","type","onKeyup"]))]),_:1})]),_:1},8,["modelValue"]),t(F,{loading:s.loading,type:"primary",style:{width:"100%","margin-bottom":"30px"},onClick:B(i.handleLogin,["prevent"])},{default:n(()=>[S("Login")]),_:1},8,["loading","onClick"]),se]),_:1},8,["model","rules"])])}const re=C(Y,[["render",te],["__scopeId","data-v-f2040ee2"]]);export{re as default};