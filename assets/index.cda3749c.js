import{A as V}from"./index.d95f35f4.js";import x from"./index.cf388ad6.js";import{A as C}from"./index.591bfa79.js";import{_ as b,u as A,g as y,L as h,r as c,o as u,j as _,w as d,A as B,U as E,e as F,h as O,a5 as S,a6 as U,c as L,a as f,B as g,F as T,a2 as j,E as k,l as w}from"./index.a74f1f35.js";import"./index.29a6cf0d.js";const D={props:["modelValue","title"],emits:["update:modelValue"],setup(a,{emit:t}){const o=A(),e=y(()=>o.getters["app/device"]!=="desktop"),i=y(()=>o.getters["app/device"]!=="desktop"?"100%":"80%"),l=h({});return{state:l,fullscreen:e,width:i,handleClose:()=>{t("update:modelValue",l.dialogVisible)},handleOpen:()=>{},updateAction:()=>{},deleteAction:()=>{},closeAction:()=>{t("update:modelValue",!1)}}}};function H(a,t,o,e,i,l){const n=c("el-dialog");return u(),_(n,{title:o.title,modelValue:o.modelValue,"onUpdate:modelValue":t[0]||(t[0]=r=>o.modelValue=r),width:e.width,"show-close":!1,fullscreen:e.fullscreen,"close-on-press-escape":!1,"close-on-click-modal":!1,"destroy-on-close":!0,onClosed:e.handleClose,onOpen:e.handleOpen,center:""},{default:d(()=>[B(a.$slots,"default")]),_:3},8,["title","modelValue","width","fullscreen","onClosed","onOpen"])}const M=b(D,[["render",H]]),N={components:{ApplyForm:V,ProcessHistory:x,ApproveForm:C,Details:M},props:["flowid","disabled"],setup(){const a=E(),t=F(),o=h({stageid:a.params.stageid,flowid:a.params.flowid,applyid:a.params.applyid,apprid:a.params.apprid,dialogVisible:!1,actionTitle:"",loading:!0,workitem:{},stageData:{}}),e=A();O(()=>{const s=S.service({lock:!0,text:"Loading",background:"rgba(0, 0, 0, 0.7)"});e.dispatch("process/load",a.params.apprid).then(()=>{o.loading=!1}).finally(()=>{s.close()})});const i=y(()=>{var s=e.getters["security/user"],p=o.workitem.ApplyStatus,m=o.workitem.ApplyUser;return s.UserId==m&&p!="\u7C3D\u6838\u5B8C\u6210"&&p!="\u64A4\u92B7"}),l=U("reload");return{state:o,revoke:()=>{j.confirm("\u78BA\u8A8D\u5F8C\uFF0C\u8868\u55AE\u5C07\u5931\u6548\uFF0C\u662F\u5426\u7E7C\u7E8C\u9032\u884C\uFF1F","Warning",{confirmButtonText:"\u78BA\u5B9A",cancelButtonText:"\u53D6\u6D88"}).then(()=>{e.dispatch("process/cancel",o.stageData).then(s=>{s.Success?(k.success("\u64A4\u92B7\u6210\u529F"),l()):k.error("\u64A4\u92B7\u5931\u6557")})})},comeback:()=>{t.go(-1)},canEdit:i}}};function P(a,t,o,e,i,l){const n=c("el-button"),r=c("el-col"),s=c("el-row"),p=c("ApplyForm"),m=c("ProcessHistory");return u(),L(T,null,[f(s,{class:"block",justify:"space-between"},{default:d(()=>[f(r,{span:12},{default:d(()=>[e.canEdit?(u(),_(n,{key:0,type:"danger",onClick:t[0]||(t[0]=v=>e.revoke())},{default:d(()=>[w("\u64A4\u92B7")]),_:1})):g("",!0)]),_:1}),f(r,{span:12,style:{"text-align":"end"}},{default:d(()=>[f(n,{type:"warning",onClick:t[1]||(t[1]=v=>e.comeback())},{default:d(()=>[w("\u8FD4\u56DE")]),_:1})]),_:1})]),_:1}),e.state.loading?g("",!0):(u(),_(p,{key:0,class:"disabled-form",flowid:e.state.flowid,disabled:!0},null,8,["flowid"])),e.state.loading?g("",!0):(u(),_(m,{key:1,applyid:e.state.applyid,apprid:e.state.apprid,disabled:!0},null,8,["applyid","apprid"]))],64)}const G=b(N,[["render",P]]);export{G as default};