import{_ as C,u as x,U as N,e as L,L as D,g as p,f as I,h as A,r as u,i as R,k as S,o as r,j as l,w as g,l as F,a as m,d as s,t as c,B as n}from"./index.a74f1f35.js";const B={components:{},setup(){const a=x(),_=N(),b=L(),e=D({tableData:[],title:_.meta.title,dialogVisible:!1,selectedRow:{}}),y=p(()=>a.getters["approveList/dataList"]),f=(i,P,T)=>{b.push("/reviewForm/"+i.FlowId+"/"+i.CurrentStage+"/"+i.ApplyId+"/"+i.DataId)},t=I(!0);A(async()=>{await d(),t.value=!1});const h=p({get:()=>a.getters["approveList/pageIndex"],set:i=>{a.commit("approveList/setPageIndex",i)}}),v=p(()=>a.getters["approveList/rowNumber"]),w=p(()=>a.getters["approveList/total"]),d=()=>{a.dispatch("approveList/getApproveList")},o=()=>{d()},k=p(()=>a.getters["app/device"]);return{state:e,list:y,hanldRowClick:f,pageIndex:h,rowNumber:v,total:w,currentPageChange:o,device:k,loading:t}}};function V(a,_,b,e,y,f){const t=u("el-table-column"),h=u("el-table"),v=u("el-pagination"),w=u("el-card"),d=R("loading");return S((r(),l(w,null,{header:g(()=>[F(" \u500B\u4EBA\u7C3D\u8FA6\u8A18\u9304 ")]),default:g(()=>[m(h,{data:e.list,ref:"singleTable","header-row-class-name":"el-custom-header",border:"","empty-text":"\u67E5\u7121\u8CC7\u6599","highlight-current-row":"",onRowClick:e.hanldRowClick,style:{width:"100%"}},{default:g(()=>[e.device==="mobile"?(r(),l(t,{key:0,type:"expand"},{default:g(o=>[s("p",null,"\u7533\u8ACB\u55AE\u865F: "+c(o.row.ApplyId),1),s("p",null,"\u8868\u55AE\u540D\u7A31: "+c(o.row.FormName),1),s("p",null,"\u968E\u6BB5\u540D\u7A31: "+c(o.row.StageName),1),s("p",null,"\u7533\u8ACB\u4EBA: "+c(o.row.ApplyUser),1),s("p",null,"\u7533\u8ACB\u65E5\u671F: "+c(o.row.ApplyDate),1),s("p",null,"\u5B8C\u6210\u65E5\u671F: "+c(o.row.FinishDate),1)]),_:1})):n("",!0),e.device!=="mobile"?(r(),l(t,{key:1,prop:"ApplyId",label:"\u7533\u8ACB\u55AE\u865F",width:"80"})):n("",!0),e.device!=="mobile"?(r(),l(t,{key:2,prop:"FormName",label:"\u8868\u55AE\u540D\u7A31",width:"140"})):n("",!0),e.device!=="mobile"?(r(),l(t,{key:3,prop:"StageName",label:"\u968E\u6BB5\u540D\u7A31"})):n("",!0),e.device!=="mobile"?(r(),l(t,{key:4,prop:"ApplyUser",label:"\u7533\u8ACB\u4EBA",width:"100"})):n("",!0),m(t,{prop:"Title",label:"\u4E3B\u65E8/\u6A19\u984C",width:"180"}),e.device!=="mobile"?(r(),l(t,{key:5,prop:"ApplyDate",label:"\u7533\u8ACB\u65E5\u671F",width:"190"})):n("",!0),m(t,{prop:"StageStatus",label:"\u7C3D\u6838\u72C0\u614B",width:"100"}),e.device!=="mobile"?(r(),l(t,{key:6,prop:"FinishDate",label:"\u5B8C\u6210\u65E5\u671F",width:"190"})):n("",!0)]),_:1},8,["data","onRowClick"]),m(v,{"page-size":e.rowNumber,"current-page":e.pageIndex,layout:"prev, pager, next",total:e.total,onCurrentChange:e.currentPageChange},null,8,["page-size","current-page","total","onCurrentChange"])]),_:1})),[[d,e.loading]])}const z=C(B,[["render",V]]);export{z as default};
