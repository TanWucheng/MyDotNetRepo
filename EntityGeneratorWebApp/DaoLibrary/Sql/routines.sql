create
    definer = ten@`%` function func_get_split_string(p_string varchar(1000), p_delimiter varchar(5), p_order int) returns varchar(256)
begin
    declare result varchar(255) default '';
    set result = reverse(substring_index(reverse(substring_index(p_string, p_delimiter, p_order)), p_delimiter, 1));
    return result;
end;



create
    definer = ten@`%` function func_get_split_string_total(p_string varchar(1000), p_delimiter varchar(5)) returns int
begin
    return 1 + (length(p_string) - length(replace(p_string, p_delimiter, '')));
end;

create
    definer = ten@`%` function func_dbo_to_entity_attr(p_string varchar(1000), p_delimiter varchar(5)) returns varchar(1000)
begin
    -- Get the separated string.
    declare v_total int default 0;
    declare v_index int default 0;
    declare v_return varchar(1000) default '';
    declare v_split varchar(256) default '';
    set v_total = func_get_split_string_total(p_string, p_delimiter);
    while v_index < v_total
        do
            set v_index = v_index + 1;
            set v_split = func_get_split_string(p_string, p_delimiter, v_index);
            set v_return = concat(v_return, upper(substr(v_split, 1, 1)), substr(v_split, 2, length(v_split) - 1));
        end while;
    return v_return;
end;

create
    definer = ten@`%` procedure sp_query_column_info(IN p_schema_name varchar(128), IN p_table_name varchar(128))
begin
    SELECT col.ORDINAL_POSITION                          as id,
           col.TABLE_NAME                                AS TableName,
           #tbl.TABLE_COMMENT                             as TableComment,
           func_dbo_to_entity_attr(col.COLUMN_NAME, '_') AS ColumnName,
           #col.COLUMN_DEFAULT                            AS ColumnDefault,
           #col.IS_NULLABLE                               AS IsNullable,
           col.DATA_TYPE                                 AS DataType,
           case
               when upper(col.DATA_TYPE) in ('TINYINT', 'SMALLINT',
                                             'MEDIUMINT', 'INT ', 'INTEGER', 'BIGINT', 'FLOAT', 'DOUBLE', 'DECIMAL')
                   then col.NUMERIC_PRECISION
               when upper(col.DATA_TYPE) in ('CHAR', 'VARCHAR', 'TINYBLOB', 'TINYTEXT')
                   then col.CHARACTER_MAXIMUM_LENGTH
               else 0 end                                as DataLength,
           #col.CHARACTER_MAXIMUM_LENGTH                  AS CharMaxLength,
           #col.NUMERIC_PRECISION                         AS NumPrecision,
           #col.NUMERIC_SCALE                             AS NumScale,
           col.COLUMN_COMMENT                            AS ColumnComment
    FROM information_schema.`COLUMNS` col
             inner join information_schema.`TABLES` tbl on col.TABLE_NAME = tbl.TABLE_NAME
    WHERE col.TABLE_SCHEMA = p_schema_name
      and tbl.TABLE_SCHEMA = p_schema_name
      and tbl.TABLE_NAME = p_table_name
    ORDER BY col.TABLE_NAME,
             col.ORDINAL_POSITION;
end;

create
    definer = ten@`%` procedure sp_query_table_info(IN p_schema_name varchar(128))
begin
    select @rowNum := @rowNum + 1                       AS id,
           tbl.TABLE_NAME                               as TableName,
           func_dbo_to_entity_attr(tbl.TABLE_NAME, '_') as EntityClassName,
           tbl.TABLE_COMMENT                            as TableComment
    from information_schema.`TABLES` tbl,
         (SELECT @rowNum := 0) row_no
    where tbl.TABLE_SCHEMA = p_schema_name;
end;