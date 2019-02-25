<template>
    <div :id="_id" class="d-inline">
        <ae-context-menu ref="contextMenu">
            <ul class="options" slot-scope="props">
                <li @click="onContextMenu(field,props)"><object type="image/svg+xml" data="images/edit.svg" width="16" height="16"></object><span style="padding-left:18px">編集する</span></li>
                <hr />
                <p><object type="image/svg+xml" data="images/info_blue.svg" width="16" height="16"></object>&ensp;グループを編集する場合、”編集する”をクリックしてください。<br /> 編集中の場合は内容が失われます。内容を保存する場合は、<br />下書き保存をご利用ください。</p>
                <!--<li @click="onContextMenu('dosuru',props.groupId)"><object type="image/svg+xml" data="images/edit.svg" width="16" height="16"></object><span style="padding-left:16px;">どうする</span></li>
            <li @click="onContextMenu('aiueo',props.groupId)"><object type="image/svg+xml" data="images/edit.svg" width="16" height="16"></object><span style="padding-left:16px;">あいうえお</span></li>-->
            </ul>
        </ae-context-menu>
        <template v-if="field.options">
            <b-button type="button"
                      :class="field.name"
                      v-for="opt in field.options"
                      :key="opt.value"
                      @click="onClick(field,opt)"
                      :style="{ 'background-color': opt.color ,'border':0, 'color': fontColor(opt.color)}">
                <div @contextmenu.prevent="$refs.contextMenu.open(arguments[0],opt.value)" style="display:inline-block">
                    <span v-html="opt.text"></span>
                </div>
            </b-button>
        </template>
    </div>
</template>

<script>
    /* ******************************************************************
    *
    * ae-tag-xxxxxxxx
    *
    * in  string    page
    * in  string    section
    * in  {}        field
    * in  {}        viewModel
    * in  {}        state
    *
    ******************************************************************* */
    import aeTag from "./ae-tag.js"
    export default {
        mixins: [aeTag],
        props: [
        ],
        data() {
            return {
            }
        },
        watch: {
        },
        methods: {

            /** *****************************************************************
            *
            * コンテキストメニュー
            *
            ******************************************************************* */
            onContextMenu(action, param) {

                //alert(JSON.stringify(action))
                //alert(JSON.stringify(param))

                this.pageLoader("auth","search")
            },
            onClick: function (field, param) {
                this.viewModel[field.name] = param.value;
                this.listener(this.section, field, param);
            },

        },
 
    }

</script>
<style scoped>
</style>
